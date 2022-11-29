using System;
using System.Threading;
using System.Threading.Tasks;
//using Firebase.Database;
//using Firebase.Extensions;
using UnityEngine;

public class VersionManager : Singleton<VersionManager>
{
    public string ApiServer { get; private set; }
    public bool IsReviewServer { get; private set; }
    public bool IsVersionOutdated { get; private set; }

    protected override void OnAwake()
    {
    }

    public Task<bool> GetVersion()
    {
        var completionSrc = new TaskCompletionSource<bool>();
        //        DatabaseReference db = FirebaseDatabase.DefaultInstance.GetReference("info");
        //        db.GetValueAsync().ContinueWithOnMainThread(task =>
        //        {
        //            if (task.IsCanceled || task.IsFaulted)
        //            {
        //                completionSrc.SetResult(false);
        //                return;
        //            }
        //​
        //            string json = task.Result.GetRawJsonValue();
        //            VersionInfo info = JsonUtility.FromJson<VersionInfo>(json);
        //            Version app = new Version(Application.version);
        //            Version review = new Version(info.review.version);
        //            Version live = new Version(info.live.minVersion);
        //​
        //            if (app == review)
        //            {
        //                IsReviewServer = true;
        //                ApiServer = info.review.address;
        //                completionSrc.SetResult(true);
        //            }
        //            else
        //            {
        //                IsVersionOutdated = app < live;
        //                ApiServer = info.live.address;
        //                completionSrc.SetResult(true);
        //            }
        //​
        //        });​
        return completionSrc.Task;
    }
}

public class VersionInfo
{
    public VersionData review;
    public VersionData live;
}

[Serializable]
public class VersionData
{
    public string address;
    public string minVersion;
    public string version;
}