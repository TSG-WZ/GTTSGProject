using Dapper;
using DTTSG_Model;

namespace DTTSG_DAL
{
    public class ImageServer : BaseServer<ImageInfo>
    {
        public ImageInfo GetimageInfo(int imageId)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@ImageId", imageId);

            string sql = @"select * from ImageInfo where ImageId = @ImageId";
            return GetModel(sql,parameters);
        }
    }
}
