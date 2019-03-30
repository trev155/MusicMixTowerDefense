using UnityEngine;
using UnityEngine.UI;

public class HarvesterPanel : MonoBehaviour {
    public Text unallocatedHarvesters;
    public Text mineralHarvesters;
    public Text gasHarvesters;

    private void Start() {
        SetUnallocatedHarvesters(GameEngine.GetInstance().unallocatedHarvesters);
        SetMineralHarvesters(GameEngine.GetInstance().mineralHarvesters);
        SetGasHarvesters(GameEngine.GetInstance().gasHarvesters);
    }

    public void SetUnallocatedHarvesters(int num) {
        unallocatedHarvesters.text = num + "";
    }

    public void SetMineralHarvesters(int num) {
        mineralHarvesters.text = num + "";
    }

    public void SetGasHarvesters(int num) {
        gasHarvesters.text = num + "";
    }
    
    public void AllocateMineralHarvester() {
        if (GameEngine.GetInstance().unallocatedHarvesters == 0) {
            GameEngine.GetInstance().messageQueue.PushMessage("Cannot allocate mineral harvester. No unallocated harvesters available", MessageType.INFO);
            return;
        }
        if (GameEngine.GetInstance().mineralHarvesters >= 25) {
            GameEngine.GetInstance().messageQueue.PushMessage("Cannot allocate mineral harvester. Reached upper limit", MessageType.INFO);
            return;
        }

        GameEngine.GetInstance().AllocateMineralHarvester();
    }

    public void DeallocateMineralHarvester() {
        if (GameEngine.GetInstance().mineralHarvesters == 0) {
            GameEngine.GetInstance().messageQueue.PushMessage("Cannot deallocate mineral harvester. No allocated mineral harvesters available", MessageType.INFO);
            return;
        }

        GameEngine.GetInstance().DeallocateMineralHarvester();
    }

    public void AllocateGasHarvester() {
        if (GameEngine.GetInstance().unallocatedHarvesters == 0) {
            GameEngine.GetInstance().messageQueue.PushMessage("Cannot allocate gas harvester. No unallocated harvesters available", MessageType.INFO);
            return;
        }
        if (GameEngine.GetInstance().gasHarvesters >= 5) {
            GameEngine.GetInstance().messageQueue.PushMessage("Cannot allocate gas harvester. Reached upper limit", MessageType.INFO);
            return;
        }

        GameEngine.GetInstance().AllocateGasHarvester();
    }

    public void DeallocateGasHarvester() {
        if (GameEngine.GetInstance().gasHarvesters == 0) {
            GameEngine.GetInstance().messageQueue.PushMessage("Cannot deallocate gas harvester. No allocated gas harvesters available", MessageType.INFO);
            return;
        }

        GameEngine.GetInstance().DeallocateGasHarvester();
    }
}
