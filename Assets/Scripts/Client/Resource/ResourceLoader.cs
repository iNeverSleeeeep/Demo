
using System.Collections;
using UnityEngine;

namespace Demo.Resources
{
    class ResourceLoader
    {

        public void Load<T>(string path, ResourceLoadFinishDelegateVoid<T> callback) where T : UnityEngine.Object
        {
            Game.Instance.coroutineManager.Start(LoadImpl(path, callback));
        }
        IEnumerator LoadImpl<T>(string path, ResourceLoadFinishDelegateVoid<T> callback) where T : UnityEngine.Object
        {
            var request = UnityEngine.Resources.LoadAsync<T>(path);
            while (request.isDone == false)
                yield return null;
            
            callback(request.asset as T);
        }
        public delegate void ResourceLoadFinishDelegateVoid<T>(T resource);


        // 不知道需不需要在加载资源的回调里面也做协程 因为二义性问题先注释掉
        //public void Load<T>(string path, ResourceLoadFinishDelegate<T> callback) where T : UnityEngine.Object
        //{
        //    Game.Instance.coroutineManager.Start(LoadImpl(path, callback));
        //}
        //IEnumerator LoadImpl<T>(string path, ResourceLoadFinishDelegate<T> callback) where T : UnityEngine.Object
        //{
        //    var request = UnityEngine.Resources.LoadAsync<T>(path);
        //    while (request.isDone == false)
        //        yield return null;
        //
        //    yield return Game.Instance.coroutineManager.Start(callback(request.asset as T));
        //}

        //public delegate IEnumerator ResourceLoadFinishDelegate<T>(T resource);
    }
}

