#!/bin/bash

for file in $(find ./ -name "*.sln"); do
    dotnet build "./$file"
done

