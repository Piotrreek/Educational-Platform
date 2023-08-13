import classes from "./Material.module.css";
import { Form, useActionData, useNavigation } from "react-router-dom";
import useInput from "../../../hooks/useInput";
import { notEmpty } from "../../../utils/validators";
import { useState, useEffect } from "react";
import useAuth from "../../../hooks/useAuth";
import Button from "../../ui/Button";

const AddOpinionForm = () => {
  const [actionDataVisible, setActionDataVisible] = useState(false);
  const { ctx } = useAuth();
  const actionData = useActionData();
  const navigation = useNavigation();
  const { value, isValid, onBlur, onChange, error, hasError, reset } =
    useInput(notEmpty);

  useEffect(() => {
    reset();
    setActionDataVisible(true);
    setTimeout(() => {
      setActionDataVisible(false);
    }, 3000);
  }, [actionData, reset]);

  const isSubmittingOpinion = navigation.state === "submitting";
  const submitError = actionData?.error;
  const submitIsSuccess = actionData?.isSuccess;
  const isLoggedIn = ctx.claims.isLoggedIn;

  return (
    <>
      {isLoggedIn && (
        <section className={classes["new-opinion"]}>
          <h2>Dodaj nową opinię</h2>
          <Form method="POST">
            <textarea
              name="opinion"
              onChange={onChange}
              onBlur={onBlur}
              value={value}
            ></textarea>
            <span className={classes.error}>{hasError && error}</span>
            {actionDataVisible && (
              <>
                <span className={classes.error}>
                  {!!submitError && submitError}
                </span>
                <span className={classes.success}>
                  {!!submitIsSuccess && "Pomyślnie dodano opinię"}
                </span>
              </>
            )}
            <Button disabled={!isValid || isSubmittingOpinion}>
              {isSubmittingOpinion ? "Dodaję" : "Dodaj"}
            </Button>
          </Form>
        </section>
      )}
    </>
  );
};

export default AddOpinionForm;
