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
using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using System;
using System.Collections.Generic;

namespace Tizen.NUI.MediaHub
{

    public class HomePageData
    {
        private List<object> dataList;


        public HomePageData()
        {
            dataList = new List<object>();
            Initialize();
        }

        private void Initialize()
        {
            for (int index = 0; index < 3; index++)
            {
                ContentModel contentModel = new ContentModel(index, "Empty", "NO." + index.ToString(), ContentItemType.eItemUpFolder, index.ToString(), "", "", 1);
                dataList.Add(contentModel);
            }

            ContentModel itemFolder = new ContentModel(3, "Data", "NO.3", ContentItemType.eItemFolder, "3", "", "", 2);
            dataList.Add(itemFolder);
        }

        public List<object> GetData()
        {
            return dataList;
        }
    }
        
}
