import { useReducer } from "react";

export const FilterAction = {
  universityId: "universityId",
  facultyId: "facultyId",
  subjectId: "subjectId",
  courseId: "courseId",
};

const entitiesInitialState = {
  universityId: "",
  facultyId: "",
  subjectId: "",
  courseId: "",
};

const reducer = (state, action) => {
  return { ...state, [action.field]: action.payload };
};

const useAcademyEntities = () => {
  const [academyEntities, dispatch] = useReducer(reducer, entitiesInitialState);

  return { academyEntities, dispatch };
};

export default useAcademyEntities;
