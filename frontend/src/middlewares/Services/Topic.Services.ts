import Topic from "../../models/Topic";
import APIGetter from "./APIGetter";

export default class TopicServices {
  static async getTopicsFromSae(saeId: string): Promise<Topic[]> {
    return await APIGetter(`Subject/sae/${saeId}`);
  }
}
