import { useEffect, useState } from "react";
import { useActionData } from "react-router-dom";

import classes from "./ActionDataMessage.module.css";

const ActionDataMessage = ({ successMessage }) => {
  const [actionDataVisible, setActionDataVisible] = useState(false);
  const actionData = useActionData();

  useEffect(() => {
    setActionDataVisible(true);
    const timeout = setTimeout(() => {
      setActionDataVisible(false);
    }, 3000);

    return () => clearTimeout(timeout);
  }, [actionData]);

  return (
    actionDataVisible && (
      <>
        {!!actionData?.error && (
          <p className={classes.error}>{actionData.error}</p>
        )}
        {!!actionData?.isSuccess && (
          <p className={classes.success}>{successMessage}</p>
        )}
      </>
    )
  );
};

export default ActionDataMessage;
