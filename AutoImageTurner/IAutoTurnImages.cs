namespace AutoImageTurner
{
    public interface IAutoTurnImages
    {
        void RotateImagesInFolder(string folder, string format);
        void RotateImagesInFolderNoMessage(string folder, string format);
    }
}