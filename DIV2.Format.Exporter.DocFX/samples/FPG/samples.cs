// FPG class samples

// Sample 1
var palette = new PAL("DIV.PAL");
var fpg = new FPG(palette);

// Sample 2
var fpg = new FPG("PLAYER.FPG");

// Sample 3
byte[] buffer = System.IO.File.ReadAllBytes("PLAYER.FPG");
var fpg = new FPG(buffer);

// Sample 4
var map = fpg[16]; // Gets the MAP in the FPG at index 16.

// Sample 5
foreach (MAP map in fpg)
{
    // Prints each MAP graphId and description values:
    Console.WriteLine($"{map.GraphId}: {map.Description}");
}

// Sample 6
var map = new MAP("RUN101.MAP");
fpg.Add(map);

// Sample 7
byte[] buffer = System.IO.File.ReadAllBytes("RUN101.MAP");
fpg.Add(buffer);

// Sample 8
fpg.Add("RUN101.MAP");

// Sample 9
var map = new MAP("RUN101.MAP");
// ...
fpg.Remove(map); // Removes the MAP that match with this instance.

// Sample 10
fpg.Remove(101); // Removes the MAP with graphId 101.

// Sample 11
fpg.RemoveAt(16); // Removes the MAP at index 16.

// Sample 12
var map = new MAP("RUN101.MAP");
bool exists = fpg.Contains(map);

// Sample 13
bool exists = fpg.Contains(101); // Checks if the FPG contains any MAP with graphId 101.

// Sample 14
// How to replace the MAP with graphId 101 on a FPG file:
var fpg = new FPG("PLAYER.FPG");

// Loads the new MAP to replace an existing one.
// For this sample, we assume that this MAP has the graphId 101:
var map = new MAP("RUN101.MAP");

// Remove the current MAP with graphId 101:
fpg.Remove(101);

// Adds the new MAP:
fpg.Add(map);

// Sample 15
fpg.Clear();

// Sample 16
fpg.Save("PLAYER.FPG");
