#!/bin/bash

MYSQL="mysql.exe"
CMD="$MYSQL -u root -p"

which $MYSQL || exit

echo " - - - Creating new Database"
$CMD < MedStaCruz.sql
[ $? -ne 0 ] && exit
echo

for i in Poblations/*; do
    echo " - - - Importing file: $i"
    echo
    $CMD -D'MedStaCruz' < $i
    [ $? -ne 0 ] && break
    echo
done