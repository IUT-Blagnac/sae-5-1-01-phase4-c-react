import Sae from "../../models/Sae";
import FetcherGET from "./Fetcher";

export default class SaeServices {
  static async getSaeInfoFromUserId(
    userId: string,
    saeId: string
  ): Promise<Sae> {
    const saes = await this.getAllSaeFromUserId(userId);
    return saes.find((sae) => sae.id === saeId) as Sae;
  }

  static async getSaeInfoFromAdminUserId(): Promise<Sae[]> {
    return (await FetcherGET(`Sae/admin`)) as Sae[];
  }

  static async getAllSaeFromUserId(userId: string): Promise<Sae[]> {
    return (await FetcherGET(`Sae/user/${userId}`)) as Sae[];
  }
}
