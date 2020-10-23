using UnityEngine;
public interface INetworkExampleService
{
    void Request();
}
public class NetworkExampleService : INetworkExampleService
{
    public void Request()
    {
        Debug.Log("请求服务器");
    }
}
