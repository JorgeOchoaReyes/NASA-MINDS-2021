//======= Copyright (c) Valve Corporation, All rights reserved. ===============
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

namespace Valve.VR.Extras
{
    public class SteamVR_LaserPointer : MonoBehaviour
    {
        public SteamVR_Behaviour_Pose pose;

        //public SteamVR_Action_Boolean interactWithUI = SteamVR_Input.__actions_default_in_InteractUI;
        public SteamVR_Action_Boolean interactWithUI = SteamVR_Input.GetBooleanAction("InteractUI");

        public bool active = true;
        public Color color;
        public Color colorEnd;
        public float thickness = 0.002f;
        public Color clickColor = Color.green;
        public GameObject holder;
        public GameObject pointer;
        public GameObject pointerEnd;
        public int controlType; 
        bool isActive = false;
        public bool addRigidBody = false;
        public Transform reference;
        public event PointerEventHandler PointerIn;
        public event PointerEventHandler PointerOut;
        public event PointerEventHandler PointerClick;

        public bool headPointer;

        Transform previousContact = null;


        private void Start()
        {
            controlType = 0;

            if (pose == null)
                pose = this.GetComponent<SteamVR_Behaviour_Pose>();
            if (pose == null)
                Debug.LogError("No SteamVR_Behaviour_Pose component found on this object", this);

            if (interactWithUI == null)
                Debug.LogError("No ui interaction action has been set on this component.", this);

            holder = new GameObject();
            holder.transform.parent = this.transform;

            //orig: Vector3.zero
            //set the finger when controltype is 1 =>
                //for right hand new Vector3(.032f, -.06f, 0.0002f) 
                //for left hand new Vector3(-.032f, -.06f, 0.0002f)
            if (transform.name == "LeftHand" && controlType == 1) holder.transform.localPosition = new Vector3(-.032f, -.06f, 0.0002f);
            else if (transform.name == "RightHand" && controlType == 1) holder.transform.localPosition = new Vector3(.032f, -.06f, 0.0002f);
            else holder.transform.localPosition = Vector3.zero; //default case

            //holder.transform.localRotation = Quaternion.Euler(45, 0, 0); this angles the phyisical pointer in the same direction as the finger 
            //original: was Quaternion.identity;
            if (transform.name == "LeftHand" && controlType == 1) holder.transform.localRotation = Quaternion.Euler(45, 0, 0);
            else if (transform.name == "RightHand" && controlType == 1) holder.transform.localRotation = Quaternion.Euler(45, 0, 0);
            else holder.transform.localRotation = Quaternion.identity; //default case



            if (!headPointer)
            {
                pointer = GameObject.CreatePrimitive(PrimitiveType.Cube);
                pointer.name = "LaserBeam";
                pointer.transform.parent = holder.transform;
                pointer.transform.localScale = new Vector3(thickness, thickness, 100f);
                pointer.transform.localPosition = new Vector3(0f, 0f,50f);

                pointer.transform.localRotation = Quaternion.identity;
                BoxCollider collider = pointer.GetComponent<BoxCollider>();
                if (addRigidBody)
                {
                    if (collider)
                    {
                        collider.isTrigger = true;
                    }
                    Rigidbody rigidBody = pointer.AddComponent<Rigidbody>();
                    rigidBody.isKinematic = true;
                }
                else
                {
                    if (collider)
                    {
                        Object.Destroy(collider);
                    }
                }
                Material newMaterial = new Material(Shader.Find("Unlit/Color"));
                newMaterial.SetColor("_Color", color);
                pointer.GetComponent<MeshRenderer>().material = newMaterial;
            }

            if (headPointer)
            {
                // The end of the pointer 
                pointerEnd = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                pointerEnd.name = "LaserEnd";
                SphereCollider sc = pointerEnd.GetComponent<SphereCollider>();
                Destroy(sc);
                pointerEnd.transform.parent = holder.transform;
                pointerEnd.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
                pointerEnd.transform.localPosition = new Vector3(0f, 0f, 0f);
                pointerEnd.transform.localRotation = Quaternion.AngleAxis(90, new Vector3(0, 0, 1));
                Material newMaterial1 = new Material(Shader.Find("Unlit/Color"));
                newMaterial1.SetColor("_Color", colorEnd);
                pointerEnd.GetComponent<MeshRenderer>().material = newMaterial1;
            }

        }

        public virtual void OnPointerIn(PointerEventArgs e)
        {
            if (PointerIn != null)
                PointerIn(this, e);
        }

        public virtual void OnPointerClick(PointerEventArgs e)
        {
            if (PointerClick != null)
                PointerClick(this, e);
        }

        public virtual void OnPointerOut(PointerEventArgs e)
        {
            if (PointerOut != null)
                PointerOut(this, e);
        }


        private void Update()
        {
            //Check for updates on controlType
            if (transform.name == "LeftHand" && controlType == 1) holder.transform.localPosition = new Vector3(-.032f, -.06f, 0.0002f);
            else if (transform.name == "RightHand" && controlType == 1) holder.transform.localPosition = new Vector3(.032f, -.06f, 0.0002f);
            else holder.transform.localPosition = Vector3.zero; //default case

            //Check for updates on controlType
            if (transform.name == "LeftHand" && controlType == 1) holder.transform.localRotation = Quaternion.Euler(45, 0, 0);
            else if (transform.name == "RightHand" && controlType == 1) holder.transform.localRotation = Quaternion.Euler(45, 0, 0);
            else holder.transform.localRotation = Quaternion.identity; //default case

            if (!isActive)
            {
                isActive = true;
                this.transform.GetChild(0).gameObject.SetActive(true);
            }

            float dist = 100f;

            Ray raycast;

            if (transform.name == "LeftHand" && controlType == 1)
            {
                var direction = Quaternion.AngleAxis(45, transform.right) * transform.forward;
                raycast = new Ray(holder.transform.position, direction);
            }
            else if (transform.name == "RightHand" && controlType == 1)
            {
                var direction = Quaternion.AngleAxis(45, transform.right) * transform.forward;
                raycast = new Ray(holder.transform.position, direction);
            }
            else
            {
                //default case
                raycast = new Ray(transform.position, transform.forward);
            }


            //Debug.DrawLine(raycast.origin, raycast.origin + raycast.direction * dist, Color.red, 25, false);

            RaycastHit hit;
            bool bHit = Physics.Raycast(raycast, out hit);

            if (previousContact && previousContact != hit.transform)
            {
                PointerEventArgs args = new PointerEventArgs();
                args.fromInputSource = pose.inputSource;
                args.distance = 0f;
                args.flags = 0;
                args.target = previousContact;
                OnPointerOut(args);
                previousContact = null;
            }
            if (bHit && previousContact != hit.transform)
            {
                PointerEventArgs argsIn = new PointerEventArgs();
                argsIn.fromInputSource = pose.inputSource;
                argsIn.distance = hit.distance;
                argsIn.flags = 0;
                argsIn.target = hit.transform;
                OnPointerIn(argsIn);
                previousContact = hit.transform;
            }
            if (!bHit)
            {
                previousContact = null;
            }
            if (bHit && hit.distance < 100f)
            {
                dist = hit.distance;
            }

            if (bHit && interactWithUI.GetStateUp(pose.inputSource))
            {
                PointerEventArgs argsClick = new PointerEventArgs();
                argsClick.fromInputSource = pose.inputSource;
                argsClick.distance = hit.distance;
                argsClick.flags = 0;
                argsClick.target = hit.transform;
                OnPointerClick(argsClick);
            }

            if (interactWithUI != null && interactWithUI.GetState(pose.inputSource))
            {
                if (!headPointer)
                {
                    pointer.transform.localScale = new Vector3(thickness * 5f, thickness * 5f, dist);
                    pointer.GetComponent<MeshRenderer>().material.color = clickColor;
                }
                else
                    pointerEnd.transform.localPosition = new Vector3(0, 0, dist);

            }
            else
            {
                if (!headPointer)
                {
                    pointer.transform.localScale = new Vector3(thickness, thickness, dist);
                    pointer.GetComponent<MeshRenderer>().material.color = color;
                }
                else
                    pointerEnd.transform.localPosition = new Vector3(0, 0, dist);
            }

            if (!headPointer)
            pointer.transform.localPosition = new Vector3(0f, 0f, dist / 2f);
        }
    }

    public struct PointerEventArgs
    {
        public SteamVR_Input_Sources fromInputSource;
        public uint flags;
        public float distance;
        public Transform target;
    }

    public delegate void PointerEventHandler(object sender, PointerEventArgs e);
}