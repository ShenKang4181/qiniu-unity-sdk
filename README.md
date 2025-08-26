# qiniu-unity-sdk
七牛云（https://www.qiniu.com）的unity-sdk

基于 qiniu.7.3.1.1

# 依赖
newtonsoft-json

https://github.com/Cysharp/UniTask

# 安装
```
https://github.com/ShenKang4181/qiniu-unity-sdk.git
```

# 使用

## 上传
```csharp
public static async UniTask<HttpResult> UploadAsync( byte[ ] data , string saveKey , IProgress<float> progress = null )
    {
        var mac = new Mac( ACCESS_KEY , SECRET_KEY );
        var putPolicy = new PutPolicy
        {
            Scope = Bucket + ":" + saveKey
        };
        putPolicy.SetExpires( 3600 );
        var jstr = putPolicy.ToJsonString( );
        var token = Auth.CreateUploadToken( mac , jstr );
        var config = new Config( )
        //{
        //    UseHttps = true ,
        //    UseCdnDomains = true
        //}
        ;
        var fu = new FormUploader( config );
        var extra = new PutExtra( );
        var result = await fu.UploadData( data , saveKey , token , extra , progress );
        Debug.Log( result );
        return result;
    }
```
