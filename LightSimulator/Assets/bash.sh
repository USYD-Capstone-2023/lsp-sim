#!/bin/bash

file="LEDCommands.txt"  # Replace with the path to your text file
line_count=$(wc -l < "$file")
start_line=1
end_line=12

while [ "$end_line" -le "$line_count" ]; do
    # Extract 12 lines and send to nc
    sed -n "${start_line},${end_line}p" "$file" 
    
    # Increment line numbers for next iteration
    start_line=$((start_line + 12))
    end_line=$((end_line + 12))
    
    # Wait for 300ms
    sleep 0.3
done

