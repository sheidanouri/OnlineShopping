public class UserSelectionViewModel
{
    public List<ClothingItem> ClothingItems { get; set; }
    public string NameFilter { get; set; }
    public string SizeFilter { get; set; }
    public string ColorFilter { get; set; }
    public string GenderFilter { get; set; }

    // Properties for dropdowns (e.g., colors, genders)
    public List<string> Colors { get; set; }
    public List<string> Genders { get; set; }

    public UserSelectionViewModel()
    {
        // Initialize lists as needed
        ClothingItems = new List<ClothingItem>();
        Colors = new List<string> { "Red", "Blue", "Green", "Black", "White" };
        Genders = new List<string> { "Male", "Female" };
    }
}