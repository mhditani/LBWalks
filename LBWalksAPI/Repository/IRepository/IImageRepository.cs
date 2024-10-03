using LBWalksAPI.Models.Domain;
using System.Net;

namespace LBWalksAPI.Repository.IRepository
{
    public interface IImageRepository
    {
       Task<Image> Upload(Image image);
    }
}
