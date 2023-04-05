using Blog_Project_Application.Models.DTOs.PostDTO;
using Models.VM.PostVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog_Project_Application.Services.PostServices
{
    public interface IPostServices
    {
        Task Create(CreatePostDTO model);
        Task Update(UpdatePostDTO model);
        Task Delete(int Id);
        Task<UpdatePostDTO> GetById(int Id);
        Task<List<PostVM>> GetPosts();
        Task<PostDetailsVM> GetPostDetails(int Id);
        Task<CreatePostDTO> CreatePost();
        Task<List<GetPostVM>> GetPostForMember();
    }
}
