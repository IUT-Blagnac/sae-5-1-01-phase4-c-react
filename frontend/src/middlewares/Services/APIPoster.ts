import API_URL, { ENV } from "../../env";
import { getFetchHeaders } from "../../utils/Utils";

export default async function APIPoster(
  url: string,
  body: BodyInit | null | undefined
): Promise<any> {
  return new Promise(async (resolve, reject) => {
    const res = await fetch(API_URL + "/api/" + url, {
      method: "POST",
      headers: getFetchHeaders(),
      body: body,
    });

    if (ENV === "dev") console.log(res);
  });
}
