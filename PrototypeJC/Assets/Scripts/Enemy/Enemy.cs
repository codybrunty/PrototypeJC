using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    private EnemyService service;
    private List<Transform> path;
    private int index;
    private float waitTime;

    private bool isAtFront => index == 0;

    public void Initialize(float waitTime, EnemyService service, List<Transform> path, int index) {
        this.waitTime = waitTime;
        this.service = service;
        this.path = path;
        this.index = index;

        StartCoroutine(CheckAdvance());
    }

    public void MoveTo(Vector3 position) {
        transform.position = position;
    }

    public void UpdateIndex(int newIndex) {
        index = newIndex;
    }

    private IEnumerator CheckAdvance() {
        while (!isAtFront) {
            yield return new WaitForSeconds(Random.Range(.25f, 1.5f));
            TryAdvance();
        }

        yield return new WaitForSeconds(waitTime);
        service.EnemyLeft(index, this);
        Destroy(gameObject);
    }

    private void TryAdvance() {
        if (index <= 0) return;

        int nextIndex = index - 1;
        if (service.IsSlotEmpty(nextIndex)) {
            transform.position = path[nextIndex].position;
            service.MoveEnemy(this, index, nextIndex);
            index = nextIndex;
        }
    }
}
