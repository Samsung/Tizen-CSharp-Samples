#!/bin/bash

rm build_result_m.txt

for file in **/*.sln; do
    echo "$file"
    sudo dotnet clean "./$file"
    sudo dotnet clean "---------------------------------------------------------------------------------------" >> build_result_m.txt
    sudo dotnet build "./$file" >> build_result_m.txt
done

