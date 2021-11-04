#!/bin/bash

for file in $(find ./ -name "*.sln"); do
    dotnet clean "./$file"
done

