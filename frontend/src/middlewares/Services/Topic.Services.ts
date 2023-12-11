import Topic from "../../models/Topic";
import FetcherGET from "./Fetcher";

export default class TopicServices {
  static async getTopicsFromSae(saeId: string): Promise<Topic[]> {
    return await FetcherGET(`Subject/sae/${saeId}`);
  }
}
