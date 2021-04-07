/*
 * Copyright (c) 2017 Samsung Electronics Co., Ltd.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 *
 */
using System;
using OpenTK;

namespace Panorama360
{
    public static class MatrixState
    {
        static float ratio = 1.78f;
        static bool HasInited = false;
        static float angleX = -14.5f, angleY = -273f, moveX = 0f, moveY = 0f, moveZ = -4.0f;
        static Matrix4 modelview = new Matrix4(), perspective = new Matrix4(), result = new Matrix4();

        /// <summary>
        /// Get rotation matrix
        /// </summary>
        /// <param name="H">window's Height</param>
        /// <param name="W">windows's Width</param>
        /// <returns>rotation matrix</returns>
        public static Matrix4 Get360Matrix(int H, int W)
        {
            if (H > W)
            {
                ratio = (float)W / H;
            }
            else
            {
                ratio = (float)H / W;
            }

            if (!HasInited)
            {
                moveZ = -1.0f;
            }

            HasInited = true;
            EsMatrixLoadIdentity(ref perspective);
            EsPerspective(ref perspective, 50f, ratio, 0.5f, 20.0f);
            EsMatrixLoadIdentity(ref modelview);
            EsTranslate(ref modelview, moveX, moveY, moveZ);
            EsRotate(ref modelview, angleX, 0.0f, 1f, 0.0f);
            EsRotate(ref modelview, angleY, 1.0f, 0.0f, 0.0f);
            EsMatrixLoadIdentity(ref result);
            result = modelview * perspective;
            return result;
        }

        /// <summary>
        /// Rotate matrix
        /// </summary>
        /// <param name="x">float x</param>
        /// <param name="y">float y</param>
        /// <param name="limit"> y axis limit angle</param>
        public static void Rotate(float x, float y, float limit)
        {
            angleX += (x * 0.5f);
            int te = (int)(angleX * 10) % 3600;
            angleX = (float)te / 10;

            if (angleY > -limit || angleY < limit)
            {
                angleY += (y * 0.5f);
            }

            te = (int)(angleY * 10) % 3600;
            angleY = (float)te / 10;

        }
        /// <summary>
        /// Initialize the identity matrix
        /// </summary>
        /// <param name="a">a matrix instance</param>
        static void EsMatrixLoadIdentity(ref Matrix4 a)
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (i == j)
                    {
                        a[i, j] = 1.0f;
                        continue;
                    }

                    a[i, j] = 0;
                }
            }
        }
        /// <summary>
        /// Perspective translation
        /// </summary>
        /// <param name="pe">Matrix to be processed</param>
        /// <param name="fovy">angle of elevation</param>
        /// <param name="aspectRatio">aspect Ratio</param>
        /// <param name="nearZ">near Z coordination</param>
        /// <param name="farZ">far Z coordination</param>
        static void EsPerspective(ref Matrix4 pe, float fovy, float aspectRatio, float nearZ, float farZ)
        {
            float frustumW, frustumH;
            frustumH = (float)Math.Tan(fovy / 360.0f * Math.PI) * nearZ;
            frustumW = frustumH * aspectRatio;
            EsFrustum(ref pe, -frustumW, frustumW, -frustumH, frustumH, nearZ, farZ);
        }
        /// <summary>
        /// Frustum matrix
        /// </summary>
        /// <param name="pe">Matrix to be processed</param>
        /// <param name="left">The X-axis coordinates on the left side of the top surface of the frustum</param>
        /// <param name="right">The X-axis coordinates on the right side of the top surface of the frustum </param>
        /// <param name="bottom">The Y-axis coordinates on the bottom side of the top surface of the frustum</param>
        /// <param name="top">The Y-axis coordinates on the top side of the top surface of the frustum</param>
        /// <param name="nearZ">The Z-axis coordinates on top surface of frustum cone</param>
        /// <param name="farZ">The Z-axis coordinates of the base of frustum cone</param>
        static void EsFrustum(ref Matrix4 pe, float left, float right, float bottom, float top, float nearZ, float farZ)
        {
            float deltaX = right - left;
            float deltaY = top - bottom;
            float deltaZ = farZ - nearZ;
            Matrix4 frust = new Matrix4();

            if ((nearZ <= 0.0f) || (farZ <= 0.0f) ||
                 (deltaX <= 0.0f) || (deltaY <= 0.0f) || (deltaZ <= 0.0f))
            {
            }

            frust.M11 = 2.0f * nearZ / deltaX;
            frust.M12 = frust.M13 = frust.M14 = 0.0f;

            frust.M22 = 2.0f * nearZ / deltaY;
            frust.M21 = frust.M23 = frust.M24 = 0.0f;

            frust.M31 = (right + left) / deltaX;
            frust.M32 = (top + bottom) / deltaY;
            frust.M33 = -(nearZ + farZ) / deltaZ;
            frust.M34 = -1.0f;

            frust.M43 = -2.0f * nearZ * farZ / deltaZ;
            frust.M41 = frust.M42 = frust.M44 = 0.0f;
            pe = frust * pe;
        }
        /// <summary>
        /// Rotate matrix
        /// </summary>
        /// <param name="result">Matrix to be processed</param>
        /// <param name="angle">Rotation Angle</param>
        /// <param name="x">The x-coordinate of the rotation axis</param>
        /// <param name="y">The y-coordinate of the rotation axis</param>
        /// <param name="z">The z-coordinate of the rotation axis</param>
        static void EsRotate(ref Matrix4 result, float angle, float x, float y, float z)
        {
            float sinAngle, cosAngle;
            float mag = (float)Math.Sqrt(x * x + y * y + z * z);

            sinAngle = (float)Math.Sin(angle * Math.PI / 180.0f);
            cosAngle = (float)Math.Cos(angle * Math.PI / 180.0f);
            if (mag > 0.0f)
            {
                float xx, yy, zz, xy, yz, zx, xs, ys, zs;
                float oneMinusCos;
                Matrix4 rotMat = new Matrix4();
                //x, y, z normalization
                x /= mag;
                y /= mag;
                z /= mag;
                xx = x * x;
                yy = y * y;
                zz = z * z;
                xy = x * y;
                yz = y * z;
                zx = z * x;
                xs = x * sinAngle;
                ys = y * sinAngle;
                zs = z * sinAngle;
                oneMinusCos = 1.0f - cosAngle;

                rotMat[0, 0] = oneMinusCos * xx + cosAngle;
                rotMat[0, 1] = oneMinusCos * xy - zs;
                rotMat[0, 2] = (oneMinusCos * zx) + ys;
                rotMat[0, 3] = 0.0F;

                rotMat[1, 0] = (oneMinusCos * xy) + zs;
                rotMat[1, 1] = (oneMinusCos * yy) + cosAngle;
                rotMat[1, 2] = (oneMinusCos * yz) - xs;
                rotMat[1, 3] = 0.0F;

                rotMat[2, 0] = (oneMinusCos * zx) - ys;
                rotMat[2, 1] = (oneMinusCos * yz) + xs;
                rotMat[2, 2] = (oneMinusCos * zz) + cosAngle;
                rotMat[2, 3] = 0.0F;

                rotMat[3, 0] = 0.0F;
                rotMat[3, 1] = 0.0F;
                rotMat[3, 2] = 0.0F;
                rotMat[3, 3] = 1.0F;

                result = rotMat * result;
            }
        }
        /// <summary>
        /// Translation matrix
        /// </summary>
        /// <param name="result">Matrix to be processed</param>
        /// <param name="tx">The distance that moving in the x direction</param>
        /// <param name="ty">The distance that moving in the y direction</param>
        /// <param name="tz">The distance that moving in the z direction</param>
        static void EsTranslate(ref Matrix4 result, float tx, float ty, float tz)
        {
            result.M41 += (result.M11 * tx + result.M21 * ty + result.M31 * tz);
            result.M42 += (result.M12 * tx + result.M22 * ty + result.M32 * tz);
            result.M43 += (result.M13 * tx + result.M23 * ty + result.M33 * tz);
            result.M44 += (result.M14 * tx + result.M24 * ty + result.M34 * tz);
        }

    }
}
