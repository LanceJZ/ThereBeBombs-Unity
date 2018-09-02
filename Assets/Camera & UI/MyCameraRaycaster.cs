using UnityEngine;

public class MyCameraRaycaster : MonoBehaviour
{
    //Layer[] LayerPriorities = {
    //    Layer.Object,
    //    Layer.Wall,
    //    Layer.Enemy,
    //    Layer.Walkable
    //};

    [SerializeField] float DistanceToBackground = 100f;
    Camera ViewCamera;

    RaycastHit RayHit;
    public RaycastHit Hit
    {
        get { return RayHit; }
    }

    //Layer LayerHit;
    //public Layer CurrentLayerHit
    //{
    //    get { return LayerHit; }
    //}

    //public delegate void OnLayerChange(Layer newLayer); // declare new delegate type
    //public event OnLayerChange LayerChange; // instantiate an observer set

    void Start()
    {
        ViewCamera = Camera.main;
    }

    void Update()
    {
        // Look for and return priority layer hit
        //foreach (Layer layer in LayerPriorities)
        //{
        //    RaycastHit? hit = RaycastForLayer(layer);

        //    if (hit.HasValue)
        //    {
        //        RayHit = hit.Value;

        //        if (LayerHit != layer) // if layer has changed
        //        {
        //            LayerHit = layer;

        //            if (LayerChange != null)
        //                LayerChange(layer); // call the delegates
        //        }

        //        LayerHit = layer;
        //        return;
        //    }
        //}

        //// Otherwise return background hit
        //RayHit.distance = DistanceToBackground;
        //LayerHit = Layer.RaycastEndStop;
        //LayerChange(LayerHit);
    }

    //RaycastHit? RaycastForLayer(Layer layer)
    //{
    //    int layerMask = 1 << (int)layer; // See Unity docs for mask formation
    //    Ray ray = ViewCamera.ScreenPointToRay(Input.mousePosition);
    //    RaycastHit hit; // used as an out parameter
    //    bool hasHit = Physics.Raycast(ray, out hit, DistanceToBackground, layerMask);

    //    if (hasHit)
    //    {
    //        return hit;
    //    }

    //    return null;
    //}
}
