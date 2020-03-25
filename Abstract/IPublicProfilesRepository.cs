using FoodStore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodStore.Abstract
{
    public interface IPublicProfilesRepository
    {
        PublicProfile GetPublicProfile(string userId); 
        PublicProfile GetPublicProfileByName(string userName); 
        PublicProfile SaveProfile(PublicProfile profile, string userId);
        PublicProfile EditProfile(PublicProfile profile, string userId);
        PublicProfile GetCommentPublicProfile(string AspUserdId);
    }
}
