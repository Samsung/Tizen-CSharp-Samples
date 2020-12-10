#!/bin/bash

# for file in $(find ./ -name "*.sln"); do
#     dotnet build "./$file"
# done
dotnet build "./Mobile/NUIView/NUIView.sln"
