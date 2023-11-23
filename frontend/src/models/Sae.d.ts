import { SAEStatus } from "../assets/enums/SAEStatus.enum";

export default interface Sae {
  id: string;
  name: string;
  description: string;
  statut: SAEStatus;
  min_student_per_group: number;
  max_student_per_group: number;
  min_group_per_subject: number;
  max_group_per_subject: number;
}
