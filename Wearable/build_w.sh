#!/bin/bash

rm build_result_w.txt

for file in $(find ./ -name "*.sln"); do
    echo "$file"
    sudo dotnet clean "./$file"
    sudo dotnet clean "---------------------------------------------------------------------------------------" >> build_result_w.txt
    sudo dotnet build "./$file" >> build_result_w.txt
done

