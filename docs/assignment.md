# TMMC programming assignment: 

Write a simple windows console application that counts the number of vertical black lines in a black and white image created using MS Paint. 4 basic test images are provided as img_1.jpg, img_2.jpg, img_3.jpg, img_4.jpg 

| Image Name | Actual Number of Lines |
|------------|----------------------|
| img_1.jpg  | 1                    |
| img_2.jpg  | 3                    |
| img_3.jpg  | 1                    |
| img_4.jpg  | 7                    |

## Image properties: 
1. Images used will always be in jpg format. 
2. Lines are created using MS Paint. The image background is default white. The lines are coloured using the bucket tool in black. 
3. The same line will exist on both the top half of the image and the bottom half as a continuous straight line. 

## Deliverable: 
### A windows console app with only the following behaviors: 
1. Console app takes exactly 1 argument.  
    - Output to console a message when invalid number of arguments are used
    - Application must not crash. Any exception must be output to console. 

2. Accept a string representing the absolute path of the test image as the input.
    - Example C:\TMMC_interview_assignment\img_1.jpg 

3. Output a number representing the number of vertical lines. 

### Source code 
Source code will be evaluated based on algorithm used, programming style, and appropriate usage of comments.

A readme.txt file containing basic info on how to use the app. <br />
(Optional) A document with a short summary stating any unexpected problems that needed to be tackled while programming.  