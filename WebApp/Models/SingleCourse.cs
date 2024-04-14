namespace WebApp.Models
{
    public class SingleCourse
    {
        public string Id { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string Author { get; set; } = null!;
        public string OriginalPrice { get; set; } = null!;
        public string? DiscountedPrice { get; set; }
        public int Hours { get; set; }
        public string? LikesInProcent { get; set; }
        public string? NumberofLikes { get; set; }
        public bool IsDigital { get; set; }
        public bool IsBestseller { get; set; }
        public string? CourseDescription { get; set; }

        public string? learn1 { get; set; }
        public string? learn2 { get; set; }
        public string? learn3 { get; set; }
        public string? learn4 { get; set; }
        public string? learn5 { get; set; }

        public string? programdetails1 { get; set; }
        public string? programdetails2 { get; set; }
        public string? programdetails3 { get; set; }
        public string? programdetails4 { get; set; }
        public string? programdetails5 { get; set; }
        public string? programdetails6 { get; set; }


    }
}
