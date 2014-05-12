using OSCALERT.component.os;

namespace Class_biz_scheduled_tasks
  {
  public class TClass_biz_scheduled_tasks
    {
    private Class_fs fs;

    //Constructor  Create()
    public TClass_biz_scheduled_tasks() : base()
      {
      fs = new Class_fs();
      }

    public void DoDailyChores(string current_working_directory_spec)
      {
      fs.DeleteCondemnedFolders(current_working_directory_spec + "/../protected/attachment");
      }

    } // end TClass_biz_scheduled_tasks

  }