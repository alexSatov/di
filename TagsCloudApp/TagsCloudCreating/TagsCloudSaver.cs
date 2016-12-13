using System.Drawing;

namespace TagsCloudApp.TagsCloudCreating
{
    public static class TagsCloudSaver
    {
        public static void SaveTagsCloudImage(Bitmap image, string filename)
        {
            image.Save(filename);
        } 
    }
}
