using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Final_Report.Models
{
    public class CommentAndRatingViewModel
    {
        public int CommentId { get; set; }
        public string Comment { get; set; }
        public int StarRating { get; set; }
        public bool IsEditMode { get; set; }
        public DateTime DateCreate { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserPictureUrl { get; set; }
        public int IDCUSTOMER { get; set; }
    }


    public class HotelDetailsViewModel
    {
        public HOTEL Hotel { get; set; }
        public List<string> ImageUrls { get; set; }
        public List<CommentAndRatingViewModel> CommentsAndRatings { get; set; }
        public double AverageRating { get; set; }
        public bool UserHasCommented { get; set; }
        public string UserComment { get; set; }
        public int UserRating { get; set; }
        public int UserCommentId { get; set; }
    }



}