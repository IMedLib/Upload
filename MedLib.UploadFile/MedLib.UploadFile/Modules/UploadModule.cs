using System;
using Nancy;
using System.Linq;
using System.IO;
using System.Text;
using Nancy.Responses;
using MedLib.Utils.Configuration;
using MedLib.Utils.Text;

namespace MedLib.UploadFile.Modules
{
	public class UploadModule:NancyModule
	{
		private readonly NLog.ILogger _logger=NLog.LogManager.GetCurrentClassLogger();
		public UploadModule ()
		{
			Post ["/upload"] = p => {
				var files = Request.Files;
				if (files == null|| files.Count()==0) {
					return "文件为空！";
				}
				var path = ConfigurationManager.AppSettings ("uploadpath");

				foreach (var uploadFile in files) {
					var name = uploadFile.Name;
					var index = name.LastIndexOf (".");
					if (index == -1) {
						name = Guid.NewGuid ().ToString ();
					} else {
						name = Guid.NewGuid ().ToString () + name.Substring (index);
					}
					using (var file = new FileStream (path + name, FileMode.Create)) {
						try {
							var bytes = StreamHelper.ToBytes(uploadFile.Value);
							file.Write (bytes, 0, bytes.Length);
						} catch (Exception exception) {
							_logger.Error(exception.Message);
						}
					}
				}
                return "上传成功！";
			};
			Get ["/upload"] = p => {
			
				return new HtmlResponse (contents: stream => {
					using (var fs = new FileStream ("/Users/ea7son/DonetProjects/MedLib.UploadFile/MedLib.UploadFile/Views/upload.html", FileMode.Open)) {
						fs.CopyTo (stream);
					}
				});
			};
		}
	}
}


