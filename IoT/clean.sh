#!/bin/bash

for file in $(find ./ -name "*.sln"); do
    dotnet clean "./$file"
done

if [ "$1" == "-a" ]; then
    echo "Remove all bin and obj folders!"
    find . -type d \( -name "bin" -o -name "obj" \) -exec rm -rf {} +
fi
