import API_URL from "../../env";
import { getFetchHeaders } from "../../utils/Utils";

export default async function FetcherGET(url: string): Promise<any> {
  return new Promise(async (resolve, reject) => {
    const res = await fetch(API_URL + "/api/" + url, {
      method: "GET",
      headers: getFetchHeaders(),
    });

    if (res.ok) {
      resolve(await res.json());
    } else {
      reject(await res.json());
    }
  });
}
