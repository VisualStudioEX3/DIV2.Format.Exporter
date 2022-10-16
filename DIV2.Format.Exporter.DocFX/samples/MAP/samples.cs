// MAP class samples

// Sample 1
var palette = new PAL("DIV.PAL");
var map = new MAP(palette, 64, 128, 12, "Test map.");

// Sample 2
var map = new MAP("PLAYER.MAP");

// Sample 3
byte[] buffer = System.IO.File.ReadAllBytes("PLAYER.MAP");
var map = new MAP(buffer);

// Sample 4
var map = MAP.FromImage("PLAYER.PNG");

// Sample 5
var palette = new PAL("DIV.PAL");
var map = MAP.FromImage("PLAYER.PNG", palette);

// Sample 6
map[42] = 224; // Writes the color index 224 in the pixel index 42.

// Sample 7
map[32, 24] = 224; // Writes the color index 224 in the pixel located in x32 y24.

// Sample 8
... // Creates a 64x64 MAP image.
var bitmap = new byte[64 * 64]; // Creates a 64x64 byte array.
... // Sets color indexes in each pixel.
map.SetBitmapArray(bitmap);

// Sample 9
byte colorIndex = map[42]; // Reads the color index from pixel index 42.

// Sample 10
byte colorIndex = map[32, 24]; // Reads the color index from pixel located in x32 y24.

// Sample 11
foreach (byte colorIndex in map)
{
    Console.WriteLine(colorIndex); // Prints the color index of the current pixel.
}

// Sample 12
byte[] bitmap = map.GetBitmapArray();

// Sample 13
Color[] texture = map.GetRGBTexture();

// Sample 14
map.Clear();

// Sample 15
ControlPoint point = map.ControlPoints[0]; // Reads the first/default control point.

// Sample 16
foreach (ControlPoint point in map.ControlPoints)
{
    Console.WriteLine(point); // Prints the Control Point coordinates.
}

// Sample 17
map.ControlPoints.Add(new ControlPoint(16, 16)); // Adds a new Control Point in x16 y16.

// Sample 18
map.ControlPoints.RemoveAt(2); // Removes the third (index 2) Control Point.

// Sample 19
map.ControlPoints.Clear();

// Sample 20
int graphId = map.GraphId;

// Sample 21
map.GraphId = 10;

// Sample 22
string description = map.Description;

// Sample 23
map.Description = "A simple description.";

// Sample 24
map.Save("NEWMAP.MAP");
