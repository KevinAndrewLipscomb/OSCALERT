// Derived from KiAspdotnetFramework/component/biz/Class~biz~~template~kicrudhelped~item.cs~template

using Class_db_oscalert_logs;
using kix;
using System;
using System.Collections;

namespace Class_biz_oscalert_logs
  {
  public class TClass_biz_oscalert_logs
    {
    private readonly TClass_db_oscalert_logs db_oscalert_logs = null;

    public TClass_biz_oscalert_logs() : base()
      {
      db_oscalert_logs = new TClass_db_oscalert_logs();
      }

    public bool Bind(string partial_spec, object target)
      {
      return db_oscalert_logs.Bind(partial_spec, target);
      }

    public void BindBaseDataList
      (
      string sort_order,
      bool be_sort_order_ascending,
      object target
      )
      {
      db_oscalert_logs.BindBaseDataList(sort_order,be_sort_order_ascending,target);
      }

    public void BindDirectToListControl(object target)
      {
      db_oscalert_logs.BindDirectToListControl(target);
      }

    public bool Delete(string id)
      {
      return db_oscalert_logs.Delete(id);
      }

    public bool Get
      (
      string id,
      out string timestamp,
      out string content
      )
      {
      return db_oscalert_logs.Get
        (
        id,
        out timestamp,
        out content
        );
      }

    public void Set
      (
      string id,
      string timestamp,
      string content
      )
      {
      db_oscalert_logs.Set
        (
        id,
        timestamp,
        content
        );
      }

    internal object Summary(string id)
      {
      return db_oscalert_logs.Summary(id);
      }

    } // end TClass_biz_oscalert_logs

  }
