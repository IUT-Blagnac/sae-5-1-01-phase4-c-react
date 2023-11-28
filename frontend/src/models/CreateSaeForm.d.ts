import Topic from "./Topic";

export default interface CreateSaeForm {
  name: string;
  description: string;
  id_groups: string[];
  id_coachs: string[];
  min_students_per_group: number;
  max_students_per_group: number;
  min_groups_per_subject: number;
  max_groups_per_subject: number;
  subjects: Topic[];
}
