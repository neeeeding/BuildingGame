using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cement : MonoBehaviour
{
    [SerializeField] private GameObject toolKeep;

    private MeshFilter myMeshFilter;
    private MeshCollider myCollider;

    private Rigidbody rigid;

    private static List<Cement> cmentList = new List<Cement>();

    private float interval = 0.2f; //퍼지는 정도

    [SerializeField] private bool hardening; //true : 굳음, false : 흐르는 중

    private bool make; //true : 새거 만드는 애, false : 그냥 지워질 애

    //private Vector3 foul; //막힌 부분 (양수)
    //private Vector3 foulMinus; //막힌 부분 (음수)

    private bool[] foul = new bool[6];

    private void Awake()
    {
        myMeshFilter = GetComponentInChildren<MeshFilter>();
        myCollider = GetComponentInChildren<MeshCollider>();
        rigid = GetComponent<Rigidbody>();

        cmentList.Add(this);
        hardening = false;

        make = true;
        rigid.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
    }

    private void Start()
    {
        transform.SetParent(toolKeep.transform, false);
    }

    private void Update()
    {
       // print($"X : {foul[0]}, Y : {foul[1]}, Z : {foul[2]}, -X : {foul[3]}, -Y : {foul[4]}, -Z : {foul[5]}");
    }

    public void CementDrop()  //시멘트 받기
    {
        transform.localScale += new Vector3(0, interval, 0);

        myCollider.sharedMesh = myMeshFilter.mesh;
        if (foul.All(x => x == false))
        {
            print("맞아...?");
            hardening = false;
            Diffuse();
            Hardening();  
        }
        else 
        {
            print("이거야");
            transform.localPosition += new Vector3(foul[0] ? foul[3]? 0 : -(interval/3) : foul[3] ? (interval / 3) : 0, 0,
                foul[2] ? foul[5] ? 0 : -(interval / 3) : foul[5] ? (interval / 3) : 0);

            transform.localScale += new Vector3(foul[0] ? foul[3] ? 0 : (interval/2) : foul[3] ? (interval / 2) : (interval / 2), -(interval / 2),
                foul[2] ? foul[5] ? 0 : (interval / 2) : foul[5] ? (interval / 2) : (interval / 2));
            myCollider.sharedMesh = myMeshFilter.mesh;
        }

    }

    public void ToolKeep(GameObject me)
    {
        toolKeep = me;
    }

    private void OnCollisionEnter(Collision collision)
    {
        print(collision.gameObject.name);
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Formwork"))
        {
            Vector3 worldPos = collision.contacts[0].point;
            Vector3 localPos = transform.InverseTransformPoint(worldPos);
            print(localPos);

            if (Mathf.Abs(localPos.x) >= Mathf.Abs(localPos.y) && Mathf.Abs(localPos.x) >= Mathf.Abs(localPos.z)) //x가 가장 큼
            {
                foul[0] = localPos.x > 0;
                foul[3] = localPos.x <= 0;
            }
            else if (Mathf.Abs(localPos.y) > Mathf.Abs(localPos.x) && Mathf.Abs(localPos.y) > Mathf.Abs(localPos.z)) //y가 가장 큼
            {
                foul[1] = localPos.y > 0;
                foul[4] = localPos.y <= 0;
            }
            else //z가 가장 큼
            {
                foul[2] = localPos.z > 0;
                foul[5] = localPos.z <= 0;
            }

            Diffuse();
            Hardening();
        }
        //else if (collision.gameObject.CompareTag("Cement"))
        //{
        //    if (!hardening)
        //    {
        //        collision.gameObject.transform.position += new Vector3(0, interval, 0);
        //    }
        //    CementAdd(collision.gameObject.GetComponent<Cement>());
        //}
    }

    private void Diffuse() //퍼지기
    {
        if (!hardening)
        {
            transform.localScale += new Vector3(interval, -interval, interval);

            myCollider.sharedMesh = myMeshFilter.mesh;
        }
    }

    private void Hardening() //굳기
    {
        hardening = true;

        rigid.constraints = RigidbodyConstraints.FreezePosition| RigidbodyConstraints.FreezeRotation;
        rigid.isKinematic = true;
    }

    private void CementAdd(Cement otherCement) //시멘트 끼리 합치기
    {
        if (otherCement == null || otherCement == this) return; //없으면 가

        Vector3 myPos = transform.position;

        Destroy(myCollider); //뺏어버리기
        myCollider = null;

        RandomCement(this, otherCement);

        Mesh addMesh = AddMeshs(myMeshFilter, otherCement.myMeshFilter);

        myCollider = gameObject.AddComponent<MeshCollider>(); //뺏은거 돌려주기
        myCollider.sharedMesh = addMesh;
        myCollider.convex = true;

        if (make)
        {
            myMeshFilter.mesh = addMesh;
            myCollider.sharedMesh = addMesh;
            Hardening();
            transform.position = myPos;
        }
        else
        {
            cmentList.Remove(this); //삭제
            Destroy(gameObject);
        }
    }

    private void RandomCement(Cement me, Cement other) //삭제 혹은 커질 애 고르기
    {
        if(make && other.make)
        {
            me.make = UnityEngine.Random.Range(0, 2) == 0 ? true : false;
            other.make = !me.make;
        }
    }

    private Mesh AddMeshs(MeshFilter me, MeshFilter other) //메쉬 만들기
    {
        CombineInstance[] combine = new CombineInstance[2];

        // 첫 번째 메쉬의 로컬 좌표를 기준으로 다른 메쉬를 변환
        Matrix4x4 myLocalMatrix = me.transform.worldToLocalMatrix;

        combine[0].mesh = me.mesh;
        combine[0].transform = Matrix4x4.identity; // 기준 메쉬는 그대로 사용

        combine[1].mesh = other.mesh;
        combine[1].transform = myLocalMatrix * other.transform.localToWorldMatrix;
        // 상대 변환을 적용하여 동일한 로컬 좌표계에서 병합

        Mesh combinedMesh = new Mesh();
        combinedMesh.CombineMeshes(combine, true, true); // transform 정보도 적용

        combinedMesh.RecalculateNormals(); // 면 정리
        combinedMesh.RecalculateBounds();  // 경계 재계산

        return combinedMesh;
    }

}
