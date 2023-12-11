import API_URL, { ENV } from "../../env";
import { getFetchHeaders } from "../../utils/Utils";

export default async function APIGetter(url: string): Promise<any> {
  return new Promise(async (resolve, reject) => {
    console.log(API_URL + "/api/" + url);

    const res = await fetch(API_URL + "/api/" + url, {
      method: "GET",
      headers: getFetchHeaders(),
    });

    try {
      if (res.ok) {
        resolve(await res.json());
      } else {
        reject(await res.json());
      }
    } catch (e) {
      if (ENV === "dev") console.log(e);
    }
  });
}
