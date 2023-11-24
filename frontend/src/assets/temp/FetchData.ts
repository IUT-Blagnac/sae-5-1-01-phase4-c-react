import Sae from "../../models/Sae";
import { SAEStatus } from "../enums/SAEStatus.enum";

/**
 * @deprecated
 * @debug
 *
 * This class is used to simulate a fetch from the backend.
 * It is used to test the frontend without having to wait for the backend to be ready.
 */
export default class FetchData {
  static async fetchSae(): Promise<Sae[]> {
    return new Promise((resolve) => {
      setTimeout(() => {
        resolve([SAE_ONE, SAE_TWO]);
      }, 1500);
    });
  }
}

const SAE_ONE: Sae = {
  id: "1",
  name: "Koh-Lanta",
  description: "Useless",
  statut: SAEStatus.LAUNCHED,
  min_student_per_group: 1,
  max_student_per_group: 2,
  min_group_per_subject: 1,
  max_group_per_subject: 2,
};

const SAE_TWO: Sae = {
  id: "2",
  name: "Slaves Narratives",
  description: "Say no to slavery",
  statut: SAEStatus.PENDING_USERS,
  min_student_per_group: 1,
  max_student_per_group: 2,
  min_group_per_subject: 1,
  max_group_per_subject: 2,
};
