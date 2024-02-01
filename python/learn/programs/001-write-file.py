

# Ask user enter a file name
filename = input("Enter a file name: ")
# Ask user enter a file content
content = input("Enter file content: ")

filename = f"{filename}.txt"
print(f"File name: {filename}")
print(f"File content: {content}, and Length: {len(content)}")


# Open file for writing
with open(filename, "w") as f:
    f.writelines(content);
    print(f"File {filename} has been created successfully!")
    