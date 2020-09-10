#!/bin/bash

rm build_result.txt

for file in $(find ./ -name "*.sln"); do
    echo "$file"
    sudo dotnet clean "./$file"
    sudo dotnet clean "---------------------------------------------------------------------------------------" >> build_result.txt
    sudo dotnet build "./$file" >> build_result.txt
done
