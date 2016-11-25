using System;

namespace PSCPortal.Portlets.Rotator.Libs
{
    public delegate void ImagePortletDelegate(object sender, ImagePortletArgs args);
    [Serializable]
    public class ImagePortletArgs : EventArgs
    {
        private ImagePortlet _imagePortlet;
        public ImagePortlet ImagePortlet
        {
            get
            {
                return _imagePortlet;
            }
        }
        private bool _isEdit;
        public bool IsEdit
        {
            get
            {
                return _isEdit;
            }
        }
        public ImagePortletArgs(ImagePortlet imagePortlet, bool isEdit)
        {
            _imagePortlet = imagePortlet;
            _isEdit = isEdit;
        }
    }
}