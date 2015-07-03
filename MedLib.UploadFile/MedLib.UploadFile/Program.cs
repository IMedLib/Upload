using Topshelf;


namespace MedLib.UploadFile
{
    class Program
	{
		public static void Main (string[] args)
		{
			HostFactory.Run (x => {
				x.Service<MyHost>(s=>{
					s.ConstructUsing(()=>new MyHost());
					s.WhenStarted(r=> r.Start());
					s.WhenStopped(r=>r.Stop());
				});
                x.RunAsLocalService();
			});
		}
	}
}
