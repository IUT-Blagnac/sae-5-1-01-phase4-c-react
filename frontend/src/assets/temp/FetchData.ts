import Sae from "../../models/Sae";
import Topic from "../../models/Topic";
import { SAEStatus } from "../enums/SAEStatus.enum";

const TIMEOUT = 400;

/**
 * @deprecated
 * @debug
 *
 * This class is used to simulate a fetch from the backend.
 * It is used to test the frontend without having to wait for the backend to be ready.
 */
export default class FetchData {
  static async fetchSaes(): Promise<Sae[]> {
    return new Promise((resolve) => {
      setTimeout(() => {
        resolve(SAEs);
      }, TIMEOUT);
    });
  }

  static async fetchSae(id: string): Promise<Sae> {
    const saeFound = SAEs.find((sae) => sae.id === id) as Sae;
    return new Promise((resolve) => {
      setTimeout(() => {
        resolve(saeFound);
      }, TIMEOUT);
    });
  }

  static async fetchTopics(id: string): Promise<Topic[]> {
    const topicFound = LINK_TOPIC_SAE.find((link) => link.sae === id) as {
      sae: string;
      topic: Topic[];
    };
    return new Promise((resolve) => {
      setTimeout(() => {
        resolve(topicFound.topic);
      }, TIMEOUT);
    });
  }
}

const SAE_ONE: Sae = {
  id: "1",
  name: "Koh-Lanta Pending Users",
  description: "Koh-Lanta description pas super utile en vraie",
  state: 1,
  min_student_per_group: 1,
  max_student_per_group: 2,
  min_group_per_subject: 1,
  max_group_per_subject: 2,
};

const SAE_TWO: Sae = {
  id: "2",
  name: "Koh-Lanta Pending Wishes",
  description: "Koh-Lanta description pas super utile en vraie",
  state: 2,
  min_student_per_group: 1,
  max_student_per_group: 2,
  min_group_per_subject: 1,
  max_group_per_subject: 2,
};

const SAE_THREE: Sae = {
  id: "3",
  name: "Koh-Lanta Launched",
  description: "Koh-Lanta description pas super utile en vraie",
  state: 3,
  min_student_per_group: 1,
  max_student_per_group: 2,
  min_group_per_subject: 1,
  max_group_per_subject: 2,
};

const SAEs = [SAE_ONE, SAE_TWO, SAE_THREE];

const TOPIC_ONE: Topic = {
  id: "1",
  name: "Slaves-Narratives",
  description: "Say no to slavery",
  categoriesId: ["PHP", "Refactor"],
};

const TOPIC_TWO: Topic = {
  id: "2",
  name: "Site Vacataires",
  description:
    "Salut les vacataires, je suis une longue description, j'ai plus d'idée",
  categoriesId: ["Tech au Choix", "From Scratch"],
};

const TOPIC_THREE: Topic = {
  id: "3",
  name: "Irit",
  description: "J'ai plus d'idée",
  categoriesId: ["Angular", "Refactor"],
};

const LINK_TOPIC_SAE = [
  {
    sae: "1",
    topic: [TOPIC_ONE, TOPIC_TWO, TOPIC_THREE],
  },
  {
    sae: "2",
    topic: [TOPIC_ONE, TOPIC_TWO],
  },
  {
    sae: "3",
    topic: [TOPIC_ONE, TOPIC_THREE],
  },
];
