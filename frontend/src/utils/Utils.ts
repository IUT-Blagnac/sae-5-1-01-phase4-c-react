import { SAEStatus } from "../assets/enums/SAEStatus.enum";

export function convertSaeStatutEnumToHText(statut: SAEStatus) {
  switch (statut) {
    case SAEStatus.PENDING_USERS:
      return "En attente du remplissage des fiches";
    case SAEStatus.PENDING_WISHES:
      return "En attente du remplissage des voeux";
    case SAEStatus.LAUNCHED:
      return "Lancée";
    case SAEStatus.LAUNCHED_OPEN_FOR_INTERNSHIP:
      return "Lancée et ouverte aux alternants";
    case SAEStatus.CLOSED:
      return "Clôturée";
    default:
      return "Statut inconnu";
  }
}

export function random(max: number) {
  return Math.floor(Math.random() * max);
}

export function cutText(str: string, max: number) {
  if (str.length > max) {
    return str.substr(0, max) + "...";
  }
  return str;
}
