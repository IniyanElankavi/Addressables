using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;
using UnityEngine.ResourceManagement.AsyncOperations;

public class LoadAsset : MonoBehaviour
{
    public Image img;
    public InputField inputfield;
    public AudioSource Audio;

    public void LoadSpriteAsset()
    {
        Addressables.LoadAssetAsync<Sprite>(inputfield.text).Completed += SpriteLoaded;
    }

    public void LoadAudioAsset()
    {
        Addressables.LoadAssetAsync<AudioClip>(inputfield.text).Completed += AudioLoaded;
    }

    public void LoadPrefabAsset()
    {
        Addressables.LoadAssetAsync<GameObject>(inputfield.text).Completed += PrefabLoaded;
    }

    void PrefabLoaded(AsyncOperationHandle<GameObject> obj)
    {
        if (obj.Status == AsyncOperationStatus.Succeeded)
        {
            GameObject shape = GameObject.FindGameObjectWithTag("Shapes");

            if (!string.IsNullOrWhiteSpace(inputfield.text))
            {
                if (shape)
                {
                    Destroy(shape);
                    CreateNewInstance(obj.Result);
                }
                else
                {
                    CreateNewInstance(obj.Result);
                }
            }
        }
        else
        {
            Debug.LogError(obj.Status);
        }
    }

    void CreateNewInstance(GameObject argPrefab)
    {
       GameObject newObject = Instantiate(argPrefab);
        newObject.transform.localPosition = new Vector3(0,0,3.5f);
    }

    void SpriteLoaded(AsyncOperationHandle<Sprite> obj)
    {
        if (obj.Status == AsyncOperationStatus.Succeeded)
        {
            ApplySpriteImage(obj.Result);
        }
        else
        {
            Debug.LogError(obj.Status);
        }
    }

    void AudioLoaded(AsyncOperationHandle<AudioClip> obj)
    {
        if (obj.Status == AsyncOperationStatus.Succeeded)
        {
            ApplyAudioToSource(obj.Result);
        }
        else
        {
            Debug.LogError(obj.Status);
        }
    }

    void ApplySpriteImage(Sprite sprite)
    {
        img.sprite = sprite;
    }

    void ApplyAudioToSource(AudioClip audio)
    {
        Audio.PlayOneShot(audio);
    }

}
