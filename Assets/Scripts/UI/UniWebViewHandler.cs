using UnityEngine;

public class UniWebViewHandler : MonoBehaviour
{
    private UniWebView _webView;

    public void OpenGoogle()
    {
        if (_webView == null)
        {
            _webView = gameObject.AddComponent<UniWebView>();
            _webView.Frame = new Rect(0, 0, Screen.width, Screen.height);
            _webView.OnShouldClose += OnWebViewShouldClose;
        }

        _webView.Load("https://google.com");
        _webView.Show();
    }

    private bool OnWebViewShouldClose(UniWebView webView)
    {
        webView.Hide();
        return true;
    }
}
