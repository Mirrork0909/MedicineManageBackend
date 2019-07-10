using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MedicineManageProject.Utils;
using Qiniu.Util;
using Qiniu.IO.Model;

namespace MedicineManageProject.Utils
{
    public class QiniuManager
    {
        public String getQiniuToken()
        {
            Mac mac = new Mac(QiniuMessage.AK, QiniuMessage.SK);
            Auth auth = new Auth(mac);
            PutPolicy putPolicy = new PutPolicy();
            putPolicy.Scope = QiniuMessage.BUCKET;
            putPolicy.SetExpires(7200 * 12);
            string token = Auth.CreateUploadToken(mac, putPolicy.ToJsonString());
            return token;
        }
    }
}
