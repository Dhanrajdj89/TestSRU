using System;
using System.Web;

namespace SportsRUsApp.Web.ViewModels
{
    public class AttachFileToPostViewModel
    {
        public HttpPostedFileBase[] Files { get; set; }
        public Guid UploadPostId { get; set; }
    }
}