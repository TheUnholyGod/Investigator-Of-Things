using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class custom_cursor : MonoBehaviour
{
    public Texture2D
        tex2d_default_cursor,       //2D Texture of default cursor
        tex2d_highlight_cursor;     //2D Texture of cursor when hovering over objects

    public CursorMode
        cur_mode;

    public Vector2
        hot_spot;

    public GameObject
        go_selection_circle;

    private GameObject
        go_on_hit_object;

    private Color
        color_original;

    private List<Color>
        list_ChildColor;


    private Shader
        shader_original;

    private List<Shader>
      list_ChildShader;

    private bool
        b_on_changed, 
        b_follow_target,
        b_on_hit;

    private float
        f_raycast_timer,
        f_raycast_timer_max;

    private Vector3
        vec3_raycast_position;

    private Ray
        r_ray;

    private RaycastHit
        rh_rayhit;

    public void SetCursorTexture(Texture2D _tex)
    {
        tex2d_default_cursor = _tex;
        Cursor.SetCursor(tex2d_default_cursor, hot_spot, cur_mode);
    }

    // Use this for initialization
    void Start()
    {
        cur_mode = CursorMode.Auto;
        hot_spot = Vector2.zero;
        go_on_hit_object = null;
        b_on_changed = false;
        b_follow_target = false;

        Cursor.SetCursor(tex2d_default_cursor, hot_spot, cur_mode);

        go_selection_circle = Instantiate(go_selection_circle);
        go_selection_circle.SetActive(false);

        f_raycast_timer_max = 1;
        f_raycast_timer = f_raycast_timer_max;

        rh_rayhit = new RaycastHit();

        list_ChildColor = new List<Color>();
        list_ChildShader = new List<Shader>();
    }

    // Update is called once per frame
    void Update()
    {
        if(f_raycast_timer > 0)
        {
            --f_raycast_timer;
        }

        if (f_raycast_timer <= 0)
        {
            if (!(Input.GetMouseButton(2) || Input.GetMouseButton(1)))
            {
                r_ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(r_ray, out rh_rayhit))
                {
                    if (rh_rayhit.collider.gameObject.GetComponent<MeshRenderer>() != null)
                    {
                        vec3_raycast_position = rh_rayhit.point;

                        if (rh_rayhit.collider.gameObject != go_on_hit_object)
                        {
                            if (go_on_hit_object != null && go_on_hit_object.GetComponent<MeshRenderer>() != null)
                            {
                                ChangeColorBackToDefault();
                            }
                            else
                            {
                                go_selection_circle.SetActive(false);
                            }

                            go_on_hit_object = rh_rayhit.collider.gameObject;

                            if (go_on_hit_object.GetComponent<MeshRenderer>().material.HasProperty("_Color"))
                            {
                                color_original = go_on_hit_object.GetComponent<MeshRenderer>().material.GetColor("_Color");                               
                            }
                            else
                                color_original = new Color(0, 0, 0, 1);

                            shader_original = go_on_hit_object.GetComponent<MeshRenderer>().material.shader;

                            MeshRenderer[] temp_array = go_on_hit_object.GetComponentsInChildren<MeshRenderer>();

                            foreach (MeshRenderer mr in temp_array)
                            {
                                if (mr.material.HasProperty("_Color"))
                                    list_ChildColor.Add(mr.material.GetColor("_Color"));
                                else
                                    list_ChildColor.Add(new Color(0, 0, 0, 1));

                                list_ChildShader.Add(mr.material.shader);
                            }
                            b_on_changed = false;
                        }

                        b_on_hit = true;
                    }
                    else if (rh_rayhit.collider.gameObject.CompareTag("Interactable"))
                    {
                        if (go_on_hit_object != null && go_on_hit_object.GetComponent<MeshRenderer>() != null)
                        {
                            ChangeColorBackToDefault();
                        }
                        else
                        {
                            go_selection_circle.SetActive(false);
                        }

                        go_on_hit_object = rh_rayhit.collider.gameObject;
                        go_selection_circle.SetActive(true);
                        b_on_changed = false;
                        b_on_hit = true;
                    }
                }
                else
                {
                    b_on_hit = false;
                }
            }

            f_raycast_timer = f_raycast_timer_max;
        }

        if (b_on_hit)
        {
            if (!b_on_changed)
            {
                //Cursor.SetCursor(tex2d_highlight_cursor, hot_spot, cur_mode);

                if (go_on_hit_object.GetComponent<MeshRenderer>() != null)
                {
                    Material material = new Material(Shader.Find("TBS/Smooth2"));
                    material.color = color_original;
                    go_on_hit_object.GetComponent<Renderer>().material = material;

                    MeshRenderer[] temp_array = go_on_hit_object.GetComponentsInChildren<MeshRenderer>();

                    int i = 0;

                    foreach(MeshRenderer mr in temp_array)
                    {
                        material = new Material(Shader.Find("TBS/Smooth2"));

                        if (i == 0)
                        {
                            ++i;
                            continue;
                        }

                        if (i < list_ChildColor.Count)
                            material.color = list_ChildColor[i];
                        else
                            break;

                        mr.material = material;

                        ++i;
                    }
                    
                }
                else
                {
                    go_selection_circle.transform.position = new Vector3(
                        go_on_hit_object.transform.position.x,
                         go_on_hit_object.transform.position.y + 0.3f,
                          go_on_hit_object.transform.position.z);

                    go_selection_circle.GetComponent<Projector>().orthographicSize = go_on_hit_object.transform.localScale.x * 0.15f;

                    b_follow_target = true;
                }

                b_on_changed = true;
            }
        }
        else
        {
            if (go_on_hit_object != null)
            {
                if (go_on_hit_object.GetComponent<MeshRenderer>() != null)
                {
                    ChangeColorBackToDefault();
                }
                else
                {
                    go_selection_circle.SetActive(false);
                    b_follow_target = false;
                }
            }

            //Cursor.SetCursor(tex2d_default_cursor, hot_spot, cur_mode);
        }

        if(b_follow_target)
        {
            if(go_selection_circle.transform.position != go_on_hit_object.transform.position)
            {
                go_selection_circle.transform.position = new Vector3(
                        go_on_hit_object.transform.position.x,
                         go_on_hit_object.transform.position.y + 0.3f,
                          go_on_hit_object.transform.position.z);
            }
        }
    }

    private void ChangeColorBackToDefault()
    {
        Material material = new Material(shader_original);
        material.color = color_original;
        go_on_hit_object.GetComponent<MeshRenderer>().material = material;

        MeshRenderer[] temp_array = go_on_hit_object.GetComponentsInChildren<MeshRenderer>();

        int i = 0;
        foreach (MeshRenderer mr in temp_array)
        {
            if(i == 0)
            {
                ++i;
                continue;
            }

            if (i < list_ChildColor.Count)
                mr.material.color = list_ChildColor[i];

            if (i < list_ChildShader.Count)
            {
                material = new Material(list_ChildShader[i]);
                mr.material = material;
            }
            else
                break;

            ++i;
        }

        list_ChildColor.Clear();
        list_ChildShader.Clear();

        go_on_hit_object = null;
        b_on_changed = false;
        b_follow_target = false;
    }

    public Vector3 GetRayCastPosition()
    {
        return vec3_raycast_position;
    }

    public GameObject GetRayCastObject()
    {
        return go_on_hit_object;
    }
}
