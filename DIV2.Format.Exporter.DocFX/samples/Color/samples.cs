// Color struct samples

// Sample 1
var color = new Color(0, 31, 63);

// Sample 2
color.red = 0;
color.green = 31;
color.blue = 63;
...
Console.Write($"Red: {color.red}, Green: {color.green}, Blue: {color.blue}");
// Red: 0, Green: 31, Blue: 63

// Sample 3
color[0] = 0;
color[1] = 31;
color[2] = 63;
...
Console.Write($"Red: {color[0]}, Green: {color[1]}, Blue: {color[2]}");
// Red: 0, Green: 31, Blue: 63

// Sample 4
bool isDACColor = color.IsDAC();

// Sample 5
Color rgbColor = color.ToRGB();

// Sample 6
Color dacColor = color.ToDAC();

// Sample 7
Color color = 33023;
Console.Write(color);
// { Color: { Red: 0, Green: 128, Blue: 255 } }

// Sample 8
int color = (int)new Color(0, 128, 255);
Console.Write(color);
// 33023
