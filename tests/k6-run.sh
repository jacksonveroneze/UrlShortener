#!/usr/bin/bash

echo "Start" && date

source k6.env && k6 run index.js

echo "end" && date